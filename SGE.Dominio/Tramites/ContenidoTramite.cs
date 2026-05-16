using SGE.Dominio.Comun;

namespace SGE.Dominio.Tramites;

public record class ContenidoTramite
{
    public string Contenido { get; }

    public ContenidoTramite(string c)
    {
        if (string.IsNullOrWhiteSpace(c))
        {
            throw new DominioException ("El contenido del trámite no puede estar vacío");
        }
        Contenido = c.Trim();
    }

    public override string ToString() => Contenido;
}
