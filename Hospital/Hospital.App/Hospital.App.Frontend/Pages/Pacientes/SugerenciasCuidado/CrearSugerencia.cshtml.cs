using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Hospital.App.Dominio;
using Hospital.App.Persistencia;

namespace Hospital.App.Frontend.Pages
{
    public class CrearSugerenciaModel : PageModel
    {
        private IRepositorioSugerenciasCuidado _repositorioSugerencias = new RepositorioSugerenciasCuidado( new Hospital.App.Persistencia.AppContext());
        private IRepositorioDiagnostico _repositorioDiagnostico = new RepositorioDiagnostico( new Hospital.App.Persistencia.AppContext());
        private IRepositorioPaciente _repositorioPaciente = new RepositorioPaciente( new Hospital.App.Persistencia.AppContext());
        private IRepositorioHistoriaClinica _repoHistoria = new RepositorioHistoriaClinica(new Hospital.App.Persistencia.AppContext());


        [BindProperty]
        public HistoriaClinica historiaClinica{get;set;}
        [BindProperty]
        public Paciente paciente {get;set;}
        public int idHistoria {get;set;}
        public int idpaciente {get;set;}
        [BindProperty]
        public DateTime fecha{ get; set; }
        [BindProperty]
        public Diagnostico diagnostico{ get; set; }
        [BindProperty]
        public SugerenciasCuidado sugerenciasCuidado{get;set;}

        public CrearSugerenciaModel()
        {}

        public ActionResult OnPost()
        {
            try{
                fecha = DateTime.Now;
                Console.WriteLine("fecha: " + fecha);
                sugerenciasCuidado.FechaHora = fecha;
                sugerenciasCuidado.HistoriaClinicaId = historiaClinica.Id;
                sugerenciasCuidado.DiagnosticoId = diagnostico.Id;
                Console.WriteLine("fecha: " + sugerenciasCuidado.FechaHora+" histclinid: " + sugerenciasCuidado.HistoriaClinicaId);
                SugerenciasCuidado sugerenciaAdicionada = _repositorioSugerencias.AddSugerenciasCuidado(sugerenciasCuidado);
            return Redirect("../SugerenciasCuidado/ListaSugerenciasCuidado?Id="+diagnostico.Id);
            }catch(System.Exception e)
            {
                ViewData["Error"] = "Error: " + e.Message;
                return Page();
            }
            
        }
        public void OnGet(int id)
        {
            diagnostico = _repositorioDiagnostico.GetDiagnostico(id);
            historiaClinica = _repoHistoria.GetHistoriaClinica(diagnostico.HistClinId);
        }
    }
}
