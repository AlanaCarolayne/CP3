using CP3.Domain;
using CP3.Repository;

namespace CP3.Services
{
    public class EmprestimoService : IEmprestimoService
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly ILivroService _livroService;
        private readonly IUsuarioService _usuarioService;
        private readonly IMultaRepository _multaRepository;

        public EmprestimoService(
            IEmprestimoRepository emprestimoRepository,
            ILivroService livroService,
            IUsuarioService usuarioService,
            IMultaRepository multaRepository)
        {
            _emprestimoRepository = emprestimoRepository;
            _livroService = livroService;
            _usuarioService = usuarioService;
            _multaRepository = multaRepository;
        }

        public async Task<bool> LivroDisponivelAsync(int livroIsbn)
        {
            var livro = await _livroService.GetLivroByISBNAsync(livroIsbn);

            if (livro == null)
                return false;

            return livro.Statusl == StatusL.Disponivel;
        }


        
        public async Task<string> RegistrarEmprestimoAsync(int usuarioId, int livroIsbn)
        {
         
            var podeEmprestar = await _usuarioService.ValidarLimiteEmprestimosAsync(usuarioId);
            if (!podeEmprestar)
                return "Usuário já possui 3 empréstimos ativos.";

            
            var livro = await _livroService.GetLivroByISBNAsync(livroIsbn);
            if (livro == null)
                return "Livro não encontrado.";


            if (livro.Statusl != StatusL.Disponivel)
                return "Livro indisponível para empréstimo.";

           
            var emprestimo = new Emprestimo
            {
                UsuarioId = usuarioId,
                LivroId = livroIsbn,
                DataEmprestimo = DateTime.Now,
                DataPrevista = DateTime.Now.AddDays(7), 
                Statuse = StatusE.Ativo
            };

            await _emprestimoRepository.AddEmprestimoAsync(emprestimo);

         
            await _livroService.AtualizarStatusAsync(livroIsbn, StatusL.Emprestado);

            return "Empréstimo realizado com sucesso.";
        }



        public async Task<string> RegistrarDevolucaoAsync(int emprestimoId)
        {
            var emprestimo = await _emprestimoRepository.GetEmprestimoByIdAsync(emprestimoId);
            if (emprestimo == null)
                return "Empréstimo não encontrado.";

            if (emprestimo.Statuse != StatusE.Ativo && emprestimo.Statuse != StatusE.Atrasado)
                return "Este empréstimo não está ativo.";

            emprestimo.DataDevolucaoReal = DateTime.Now;

            decimal multa = 0;

   
            if (emprestimo.DataDevolucaoReal > emprestimo.DataPrevista)
            {
                var diasAtraso = (emprestimo.DataDevolucaoReal.Value - emprestimo.DataPrevista).Days;
                multa = diasAtraso * 1;

                var multaObj = new Multa
                {
                    EmprestimoId = emprestimoId,
                    ValorMulta = (int)multa,
                    Statusm = StatusM.Pendente
                };

                await _multaRepository.RegistrarPagamentoMultaAsync(emprestimoId, multa);


                emprestimo.Statuse = StatusE.Atrasado;
            }
            else
            {
                emprestimo.Statuse = StatusE.Finalizado;
            }

   
            await _emprestimoRepository.UpdateEmprestimoAsync(emprestimo);

            await _livroService.AtualizarStatusAsync(emprestimo.LivroId, StatusL.Disponivel);

            return multa > 0
                ? $"Devolução registrada com multa de R$ {multa}."
                : "Devolução registrada sem multa.";
        }


  
        public async Task AtualizarStatusEmprestimoAsync(int emprestimoId)
        {
            var emprestimo = await _emprestimoRepository.GetEmprestimoByIdAsync(emprestimoId);

            if (emprestimo == null)
                throw new Exception("Empréstimo não encontrado.");

            if (emprestimo.DataDevolucaoReal == null)
                emprestimo.Statuse = StatusE.Ativo;
            else if (emprestimo.DataDevolucaoReal > emprestimo.DataPrevista)
                emprestimo.Statuse = StatusE.Atrasado;
            else
                emprestimo.Statuse = StatusE.Finalizado;

            await _emprestimoRepository.UpdateEmprestimoAsync(emprestimo);
        }
    }
}
