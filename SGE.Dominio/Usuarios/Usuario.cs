using SGE.Dominio.Comun;

namespace SGE.Dominio.Usuarios;

public class Usuario
{
    public Guid Id { get; private set;}
    public string Nombre { get; private set;}
    public Correo CorreoElectronico { get; private set;}
    public string ContraseñaHash { get; private set;}
    public bool EsAdministrador { get; private set;} = false;
    private readonly List<Permiso> _permisos = new();
    public IEnumerable<Permiso> Permisos => _permisos;

    private Usuario (Guid id, string nombre, Correo correo, string contraHash)
    {
        if (id == Guid.Empty)
        {
            throw new DominioException ("El Id del usuario no puede ser un Guid vacío");
        }
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new DominioException ("El nombre del usuario no puede estar vacío");
        }
        if (string.IsNullOrWhiteSpace(contraHash))
        {
            throw new DominioException ("La contraseña del usuario es obligatoria");
        }
        
        CorreoElectronico = correo ?? throw new DominioException ("El email del usuario es obligatorio");
        Id = id;
        Nombre = nombre;
        ContraseñaHash = contraHash;
    }

    public Usuario (string nombre, Correo correo, string contraHash) 
                : this (Guid.NewGuid(), nombre, correo, contraHash)  { }

    protected Usuario () {}

    public bool TienePermiso(Permiso permiso)
    {
        if (!Enum.IsDefined(typeof(Permiso), permiso))
        {
            throw new DominioException ("El permiso no es válido");
        }
        return EsAdministrador || _permisos.Contains(permiso);
    }

    public void ModificarPermisos(IEnumerable<Permiso> permisos)
    {
        _permisos.Clear();

        foreach(var permiso in permisos)
        {
            _permisos.Add(permiso);
        }
    }

    public void HacerAdministrador()
    {
        EsAdministrador = true;
    }

    public void QuitarAdministrador()
    {
        EsAdministrador = false;
    }

    public void CambiarNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new DominioException ("El nombre del usuario no puede estar vacío");
        }
        
        Nombre = nombre;
    }

    public void CambiarCorreo(Correo correo)
    {
        CorreoElectronico = correo ?? throw new DominioException ("El email del usuario es obligatorio");
    }

    public void CambiarContraseñaHash(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
        {
            throw new DominioException ("La contraseña del usuario es obligatoria");
        }

        ContraseñaHash = hash;
    }   

}