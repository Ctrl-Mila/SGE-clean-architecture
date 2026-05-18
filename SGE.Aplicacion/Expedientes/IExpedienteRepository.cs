using SGE.Dominio.Expedientes;

namespace SGE.Aplicacion.Expedientes;
public interface IExpedienteRepository
{
    void Agregar(Expediente e);
    Expediente? ObtenerPorId(Guid id);
    void Modificar(Expediente e);
    void Eliminar(Guid id);
    IEnumerable<Expediente> ObtenerTodos ();
}