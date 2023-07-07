public static class ApiConstants
{
    public const string ApiVersion = "v1";
    public const string EndPoint = $"/api/{ApiVersion}/";

    // ROUTES
    public const string UsersRoute = "users";
    public const string AuthRoute = "auth";
    public const string LoginRoute = "login";
    public const string RefreshTokenRoute = "refresh-token";
    public const string RevokeTokenRoute = "revoke";

    public const string DEFAULT_CORS_POLICY = "default";
}