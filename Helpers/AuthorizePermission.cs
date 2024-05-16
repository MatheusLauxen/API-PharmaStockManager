using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PharmaStock___API.Helpers;

public class AuthorizePermission : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly string _requiredPermission;

    public AuthorizePermission(string requiredPermission)
    {
        _requiredPermission = requiredPermission;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (!user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var permissionClaim = user.Claims.FirstOrDefault(c => c.Type == "Permissao");

        if (permissionClaim == null || permissionClaim.Value != _requiredPermission)
        {
            context.Result = new ForbidResult();
            var serviceResponse = new ServiceResponse<string>
            {
                mensagem = "Usuário não tem permissão para acessar este recurso.",
                sucesso = false
            };
            context.HttpContext.Response.StatusCode = 403; 
            context.Result = new JsonResult(serviceResponse);
        }
    }
}
