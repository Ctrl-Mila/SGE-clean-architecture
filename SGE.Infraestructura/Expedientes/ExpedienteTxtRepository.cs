using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Expedientes;
using SGE.Infraestructura.Comun;

namespace SGE.Infraestructura.Expedientes;

public class ExpedienteTxtRepository : IExpedienteRepository
{
    private readonly string _nombreArchivo = "expedientes.txt";
    public void Agregar(Expediente e)
    {
        using var sw = new StreamWriter (_nombreArchivo, true);
        sw.WriteLine(e.Id);
        sw.WriteLine(e.Carat.NombreCaratula);
        sw.WriteLine(e.FechaCreacion);
        sw.WriteLine(e.FechaUltimaModificacion);
        sw.WriteLine(e.UsuarioUltimoCambio);
        sw.WriteLine(e.Estado);
    }

    public void Modificar(Expediente e)
    {
        List<Expediente> expedientes = ObtenerTodos().ToList();
        bool encontrado = false;

        for (int i = 0; i < expedientes.Count; i++)
        {
            if (expedientes[i].Id == e.Id)
            {
                expedientes[i] = e;
                encontrado = true;
                break;
            }
        }

        if (!encontrado)
        {
            throw new RepositorioException ("El expediente a modificar no existe");
        }

        ReescribirArchivo (expedientes);
    }

    public void Eliminar(Guid id)
    {
        List<Expediente> expedientes = ObtenerTodos().ToList();
        Expediente? eliminado = null;

        foreach (Expediente e in expedientes)
        {
            if (e.Id == id)
            {
                eliminado = e;
                break;
            }
        }

        if (eliminado == null)
        {
            throw new RepositorioException("El expediente a eliminar no existe");
        }

        expedientes.Remove(eliminado);

        ReescribirArchivo (expedientes);
    }
    
    public Expediente? ObtenerPorId(Guid id)
    {
        foreach(Expediente e in ObtenerTodos())
        {
            if (e.Id == id)
            {
                return e;
            }
        }

        return null;
    }

    public IEnumerable<Expediente> ObtenerTodos ()
    {
        var resultado = new List<Expediente>();

        if (!File.Exists(_nombreArchivo))
        {
            return resultado;
        }

        using var sr = new StreamReader(_nombreArchivo);

        while (!sr.EndOfStream)
        {
            var idexp = sr.ReadLine() ?? "";
            var caratula = sr.ReadLine() ?? "";
            var fc = sr.ReadLine() ?? "";
            var fu = sr.ReadLine() ?? "";
            var usuario = sr.ReadLine() ?? "";
            var estado = sr.ReadLine() ?? "";

            Guid id = Guid.Parse(idexp);
            Caratula c = new Caratula(caratula);
            DateTime fechaCreacion = DateTime.Parse(fc);
            DateTime fechaUlt = DateTime.Parse(fu);
            Guid ultUsuario = Guid.Parse(usuario);
            EstadoExpediente est = Enum.Parse<EstadoExpediente>(estado);

            var expediente = Expediente.Reconstruir(id, c, fechaCreacion, fechaUlt, ultUsuario, est);

            resultado.Add(expediente);
        }

        return resultado;
    }

    private void ReescribirArchivo (IEnumerable<Expediente> expedientes)
    {
        using var sw = new StreamWriter(_nombreArchivo);

        foreach (Expediente e in expedientes)
        {
            sw.WriteLine(e.Id);
            sw.WriteLine(e.Carat.NombreCaratula);
            sw.WriteLine(e.FechaCreacion);
            sw.WriteLine(e.FechaUltimaModificacion);
            sw.WriteLine(e.UsuarioUltimoCambio);
            sw.WriteLine(e.Estado);
        }
    }

}