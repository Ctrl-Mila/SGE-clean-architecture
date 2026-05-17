using SGE.Dominio.Comun;
using SGE.Dominio.Expedientes;
using SGE.Dominio.Tramites;

public class Expediente
{
    public Guid Id { get; private set; }
    public Caratula Carat { get; private set; }
    public DateTime FechaCreacion { get; private set; }
    public DateTime FechaUltimaModificacion { get; private set; }
    public Guid UsuarioUltimoCambio { get; private set; }
    public EstadoExpediente Estado { get; private set; } 
    private Expediente (Guid id, Caratula c, DateTime fc, DateTime fu, Guid usuarioMod, EstadoExpediente e)
    {
        if (id == Guid.Empty)
        {
            throw new DominioException ("El Id del expediente no puede ser un Guid vacío");
        }
        if(fu < fc)
        {
            throw new DominioException ("La fecha de última modificación no puede ser menor a la fecha de creación");
        }
        if (usuarioMod == Guid.Empty)
        {
            throw new DominioException ("El identificador del usuario que realizó la última modificación no puede ser un Guid vacío");
        }
        if (!Enum.IsDefined (typeof(EstadoExpediente), e))
        {
            throw new DominioException ("El estado del expediente no es válido");
        }
        
        Carat = c ?? throw new DominioException ("La carátula es obligatoria");
        Id = id;
        FechaCreacion = fc;
        FechaUltimaModificacion = fu;
        UsuarioUltimoCambio = usuarioMod;
        Estado = e;
    }
    public Expediente (Caratula c, Guid usuarioMod) 
            : this(Guid.NewGuid(), c, DateTime.Now, DateTime.Now, usuarioMod, EstadoExpediente.RecienIniciado) {}

    public static Expediente Reconstruir (Guid id, Caratula c, DateTime fc, DateTime fu, Guid usuarioMod, EstadoExpediente e)
    {
        return new Expediente(id, c, fc, fu, usuarioMod, e);
    } 

    public void ModificarCaratula (Caratula nuevaCaratula, Guid idUsuario)
    {
        if (idUsuario == Guid.Empty)
        {
            throw new DominioException ("El identificador del usuario que realizó la última modificación no puede ser un Guid vacío");
        }
        Carat = nuevaCaratula ?? throw new DominioException ("La carátula es obligatoria");
        UsuarioUltimoCambio = idUsuario;
        FechaUltimaModificacion = DateTime.Now;
    }

    public bool ActualizarEstado(EtiquetaTramite? ultimaEtiqueta, Guid idUsuario)
    {
        if (idUsuario == Guid.Empty)
        {
            throw new DominioException ("El identificador del usuario que realizó la última modificación no puede ser un Guid vacío");
        }
        
        EstadoExpediente nuevoEstado = Estado;

        if (ultimaEtiqueta == null)
        {
            nuevoEstado = EstadoExpediente.RecienIniciado;
        }
        else if (ultimaEtiqueta == EtiquetaTramite.PaseAEstudio)
        {
            nuevoEstado = EstadoExpediente.ParaResolver;
        }
        else if (ultimaEtiqueta == EtiquetaTramite.Resolucion)
        {
            nuevoEstado = EstadoExpediente.ConResolucion;
        }
        else if(ultimaEtiqueta == EtiquetaTramite.PaseAlArchivo)
        {
            nuevoEstado = EstadoExpediente.Finalizado;
        }

        if (nuevoEstado == Estado)
        {
            return false;
        }

        Estado = nuevoEstado;
        UsuarioUltimoCambio = idUsuario;
        FechaUltimaModificacion = DateTime.Now;
        return true;
    }

    public void CambiarEstado(EstadoExpediente nuevoEstado, Guid idUsuario)
    {
        if (!Enum.IsDefined(typeof(EstadoExpediente), nuevoEstado))
        {
            throw new DominioException ("El estado del expediente no es válido");
        }
        if (idUsuario == Guid.Empty)
        {
            throw new DominioException ("El identificador del usuario que realizó la última modificación no puede ser un Guid vacío");
        }
        Estado = nuevoEstado;
        UsuarioUltimoCambio = idUsuario;
        FechaUltimaModificacion = DateTime.Now;
    }
}