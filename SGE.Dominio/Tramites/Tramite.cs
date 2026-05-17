using SGE.Dominio.Comun;
using SGE.Dominio.Tramites;

public class Tramite
{
    public Guid Id { get; private set;}
    public ContenidoTramite Contenido { get; private set; }
    public Guid ExpedienteId { get; private set; }
    public EtiquetaTramite Etiqueta { get; private set; } 
    public DateTime FechaCreacion { get; private set; }
    public DateTime FechaUltimaModificacion { get; private set; }
    public Guid UsuarioUltimoCambio { get; private set; }

    private Tramite (Guid id, ContenidoTramite contenido, Guid expid, EtiquetaTramite etiqueta, DateTime fc, DateTime fu, Guid usuario)
    {
        if (id == Guid.Empty)
        {
            throw new DominioException ("El Id del trámite no puede ser un Guid vacío");
        }
        if (expid == Guid.Empty)
        {
            throw new DominioException ("El Id del expediente no puede ser un Guid vacío");
        }
        if (usuario == Guid.Empty)
        {
            throw new DominioException ("El identificador del usuario que realizó la última modificación no puede ser un Guid vacío");
        }
        if(fu < fc)
        {
            throw new DominioException ("La fecha de última modificación no puede ser menor a la fecha de creación");
        }
        if (!Enum.IsDefined(typeof(EtiquetaTramite), etiqueta))
        {
            throw new DominioException ("La etiqueta del trámite no es válida");
        }

        Contenido = contenido ?? throw new DominioException ("El contenido del trámite es obligatorio");
        Id = id;
        ExpedienteId = expid;
        Etiqueta = etiqueta;
        FechaCreacion = fc;
        FechaUltimaModificacion = fu;
        UsuarioUltimoCambio = usuario;
    }
    public Tramite (ContenidoTramite c, Guid expid, Guid usuario, EtiquetaTramite etiqueta) 
            : this(Guid.NewGuid(), c, expid, etiqueta, DateTime.Now, DateTime.Now, usuario) {}

    public static Tramite Reconstruir (Guid id, ContenidoTramite contenido, Guid expid, EtiquetaTramite etiqueta, DateTime fc, DateTime fu, Guid usuario)
    {
        return new Tramite (id, contenido, expid, etiqueta, fc, fu, usuario);
    }

    public void Modificar (Guid idUsuario, EtiquetaTramite nuevaEtiqueta, ContenidoTramite nuevoContenido)
    {
        if (idUsuario == Guid.Empty)
        {
            throw new DominioException ("El identificador del usuario que realizó la última modificación no puede ser un Guid vacío"); 
        }
        if (!Enum.IsDefined(typeof(EtiquetaTramite), nuevaEtiqueta))
        {
            throw new DominioException ("La etiqueta del trámite no es válida");
        }
        
        Contenido = nuevoContenido ?? throw new DominioException ("El contenido del trámite es obligatorio");
        Etiqueta = nuevaEtiqueta;
        UsuarioUltimoCambio = idUsuario;
        FechaUltimaModificacion = DateTime.Now;
    }

}