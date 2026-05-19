using SGE.Aplicacion.Tramites;
using SGE.Dominio.Tramites;
using SGE.Infraestructura.Comun;

namespace SGE.Infraestructura.Tramites;

public class TramiteTxtRepository : ITramiteRepository
{
    private readonly string _nombreArchivo = "tramites.txt";
    public void Agregar (Tramite t)
    {
        using var sw = new StreamWriter(_nombreArchivo, true);

        sw.WriteLine(t.Id);
        sw.WriteLine(t.Contenido.Contenido);
        sw.WriteLine(t.ExpedienteId);
        sw.WriteLine(t.Etiqueta);
        sw.WriteLine(t.FechaCreacion);
        sw.WriteLine(t.FechaUltimaModificacion);
        sw.WriteLine(t.UsuarioUltimoCambio);
    }

    public void Modificar (Tramite t)
    {
        var tramites = ObtenerTodos().ToList();
        bool encontrado = false;

        for (int i = 0; i < tramites.Count; i++)
        {
            if (tramites[i].Id == t.Id)
            {
                tramites[i] = t;
                encontrado = true;
                break;
            }
        }

        if (!encontrado)
        {
            throw new RepositorioException("El trámite a modificar no existe");
        }

        ReescribirArchivo(tramites);
    }

    public void Eliminar (Guid id)
    {
        var tramites = ObtenerTodos().ToList();
        Tramite? eliminado = null;

        foreach (Tramite t in tramites)
        {
            if (t.Id == id)
            {
                eliminado = t;
                break;
            }
        }

        if (eliminado == null)
        {
            throw new RepositorioException("El trámite a eliminar no existe");
        }

        tramites.Remove(eliminado);

        ReescribirArchivo(tramites);
    }

    public Tramite? ObtenerPorId (Guid id)
    {
        foreach (Tramite t in ObtenerTodos())
        {
            if (t.Id == id)
            {
                return t;
            }
        }

        return null;
    }

    public IEnumerable<Tramite> ObtenerPorExpedienteId(Guid expid)
    {
        var resultado = new List<Tramite>();

        foreach (Tramite t in ObtenerTodos())
        {
            if (t.ExpedienteId == expid)
            {
                resultado.Add(t);
            }
        }

        return resultado;
    }

    public IEnumerable<Tramite> ObtenerTodos()
    {
        var resultado = new List<Tramite>();

        if (!File.Exists(_nombreArchivo))
        {
            return resultado;
        }

        using var sr = new StreamReader(_nombreArchivo);

        while (!sr.EndOfStream)
        {
            var idtramite = sr.ReadLine() ?? "";
            var contenido = sr.ReadLine() ?? "";
            var idexpediente = sr.ReadLine() ?? "";
            var etiqueta = sr.ReadLine() ?? "";
            var fc = sr.ReadLine() ?? "";
            var fu = sr.ReadLine() ?? "";
            var usuario = sr.ReadLine() ?? "";

            Guid id = Guid.Parse(idtramite);
            ContenidoTramite c = new ContenidoTramite(contenido);
            Guid expid = Guid.Parse(idexpediente);
            EtiquetaTramite et = Enum.Parse<EtiquetaTramite>(etiqueta);
            DateTime fechaCreacion = DateTime.Parse(fc);
            DateTime fechaUltima = DateTime.Parse(fu);
            Guid usuarioUltimo = Guid.Parse(usuario);

            var tramite = Tramite.Reconstruir(id, c, expid, et, fechaCreacion, fechaUltima, usuarioUltimo);

            resultado.Add(tramite);
        }

        return resultado;
    }

    private void ReescribirArchivo(IEnumerable<Tramite> tramites)
    {
        using var sw = new StreamWriter(_nombreArchivo);

        foreach (Tramite t in tramites)
        {
            sw.WriteLine(t.Id);
            sw.WriteLine(t.Contenido.Contenido);
            sw.WriteLine(t.ExpedienteId);
            sw.WriteLine(t.Etiqueta);
            sw.WriteLine(t.FechaCreacion);
            sw.WriteLine(t.FechaUltimaModificacion);
            sw.WriteLine(t.UsuarioUltimoCambio);
        }  
    }
    
}