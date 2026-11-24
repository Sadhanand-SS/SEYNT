namespace SEYNT.API.Middleware;

/// <summary>
/// Marks an action (or controller) as requiring <b>authentication only</b>: a valid JWT, but no
/// module rights. Every other controller is an RBAC module checked by <see cref="RbacMiddleware"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public sealed class AuthenticatedOnlyAttribute : Attribute;
