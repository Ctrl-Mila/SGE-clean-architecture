using SGE.Aplicacion.Expedientes;

namespace SGE.Aplicacion.Servicios;

public class ActualizacionEstadoExpedienteService (ITramiteRepository repoTramite, IExpedienteRepository repoExpediente)
{
    public void RevisionDeActualizacion (Guid idExpediente, Guid idUsuario)
    {
        var expediente = repoExpediente.ObtenerPorId(idExpediente) ?? throw new EntidadNoEncontradaException ("El expediente no fue encontrado");
        
        IEnumerable<Tramite> tramites = repoTramite.ObtenerPorExpedienteId(idExpediente);

        Tramite? ultimo = null;
        
        foreach (Tramite t in tramites)
        {
            if (ultimo == null || t.FechaCreacion > ultimo.FechaCreacion)
            {
                ultimo = t;
            }
        }

        bool cambio = expediente.ActualizarEstado(ultimo?.Etiqueta, idUsuario);

        if (cambio)
        {
            repoExpediente.Modificar(expediente);
        }

    }

}