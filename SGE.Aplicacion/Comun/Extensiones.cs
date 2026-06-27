using Microsoft.Extensions.DependencyInjection;
using SGE.Aplicacion.Expedientes.CasosDeUsoExpediente;
using SGE.Aplicacion.Servicios;
using SGE.Aplicacion.Tramites.CasosDeUsoTramite;
using SGE.Aplicacion.Usuarios.CasosDeUsoUsuario;

namespace SGE.Aplicacion.Comun;

public static class Extensiones
{
    public static IServiceCollection AddAplicacion (this IServiceCollection servicios)
    {
        // Expedientes
        
        servicios.AddScoped<AgregarExpedienteUseCase>();
        servicios.AddScoped<ModificarCaratulaExpedienteUseCase>();
        servicios.AddScoped<ModificarEstadoExpedienteUseCase>();
        servicios.AddScoped<EliminarExpedienteUseCase>();
        servicios.AddScoped<ListarTodosLosExpedientesUseCase>();
        servicios.AddScoped<ObtenerExpedientePorIdUseCase>();

        // Trámites
        
        servicios.AddScoped<AgregarTramiteUseCase>();
        servicios.AddScoped<ModificarTramiteUseCase>();
        servicios.AddScoped<EliminarTramiteUseCase>();
        servicios.AddScoped<ListarTramitesUseCase>();
        servicios.AddScoped<ObtenerTramitePorIdUseCase>();

        // Usuarios
        
        servicios.AddScoped<RegistrarUsuarioUseCase>();
        servicios.AddScoped<LoginUseCase>();
        servicios.AddScoped<ModificarMisDatosUseCase>();
        servicios.AddScoped<ListarUsuariosUseCase>();
        servicios.AddScoped<ObtenerUsuarioPorIdUseCase>();
        servicios.AddScoped<EliminarUsuarioUseCase>();
        servicios.AddScoped<ModificarPermisosUsuarioUseCase>();

        // Servicios de Aplicación
        
        servicios.AddScoped<ActualizacionEstadoExpedienteService>();

        return servicios;
    }
}