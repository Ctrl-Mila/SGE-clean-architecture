using SGE.Aplicacion.Tramites.TramiteDTOs;

namespace SGE.Aplicacion.Tramites.CasosDeUsoTramite;

public class ListarTramitesUseCase (ITramiteRepository repositorio)
{
    public ListarTramitesResponse Ejecutar (ListarTramitesRequest request)
    {
        IEnumerable<Tramite> tramites = repositorio.ObtenerPorExpedienteId(request.IdExpediente);

        var lista = new List<TramiteResponse>();

        foreach (Tramite t in tramites)
        {
            lista.Add(new TramiteResponse(t.Id, t.Contenido.Contenido, t.ExpedienteId, t.Etiqueta, t.FechaCreacion, t.FechaUltimaModificacion, t.UsuarioUltimoCambio));
        }
        
        return new ListarTramitesResponse(lista);
    }
}