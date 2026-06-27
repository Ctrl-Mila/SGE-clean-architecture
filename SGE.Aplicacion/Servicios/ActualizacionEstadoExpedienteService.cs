using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Tramites;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Servicios;

public class ActualizacionEstadoExpedienteService (ITramiteRepository repoTramite, IExpedienteRepository repoExpediente)
{
    public void RevisionDeActualizacion (Guid idExpediente, Guid idUsuario)
    {
        var expediente = repoExpediente.ObtenerPorId(idExpediente) ?? throw new EntidadNoEncontradaException ("El expediente no fue encontrado");
        
        var tramites = repoTramite.ObtenerPorExpedienteId(idExpediente);

        Tramite? ultimo = null;
        
        foreach (var t in tramites)
        {
            if (ultimo == null || t.FechaCreacion > ultimo.FechaCreacion)
            {
                ultimo = t;
            }
        }

        expediente.ActualizarEstado(ultimo?.Etiqueta, idUsuario);

    }

}