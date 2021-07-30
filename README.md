## Admin Area - Identity Base
Esempio di progetto MVC Core con autenticazione mediante Identity scaffolder e grafica gestita da un template bootstrap


## Comando per lo scaffolding di Identity
dotnet aspnet-codegenerator identity --files "Account._StatusMessage;Account.AccessDenied;Account.ConfirmEmail;Account.ConfirmEmailChange;Account.ForgotPassword;Account.ForgotPasswordConfirmation;Account.Lockout;Account.Login;Account.Logout;Account.Manage._Layout;Account.Manage._ManageNav;Account.Manage._StatusMessage;Account.Manage.ChangePassword;Account.Manage.DeletePersonalData;Account.Manage.DownloadPersonalData;Account.Manage.Email;Account.Manage.Index;Account.Manage.PersonalData;Account.Manage.SetPassword;Account.Register;Account.RegisterConfirmation;Account.ResetPassword;Account.ResetPasswordConfirmation" --dbContext AdminArea_IdentityBase.Models.Services.Infrastructure.AdminAreaDbContext --useSqLite
