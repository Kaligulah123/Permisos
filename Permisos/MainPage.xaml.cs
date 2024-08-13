

namespace Permisos
{
    public partial class MainPage : ContentPage
    {     
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }



        private async void OnRequestPermissionClicked(object sender, EventArgs e)
        {
            var isGranted = await RequestStoragePermissions();

            if (isGranted)
            {
                // Permiso concedido, puedes acceder al almacenamiento interno.
                await DisplayAlert("Permiso", "Permiso de almacenamiento concedido", "OK");

                count = 0;
            }
            else
            {
                // Permiso denegado, manejar el caso apropiadamente.
                await DisplayAlert("Permiso", "Permiso de almacenamiento denegado", "OK");

                count++;
            }
          
        }

        private async Task<bool> RequestStoragePermissions()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if (status != PermissionStatus.Granted)
            {
                //if (Permissions.ShouldShowRationale<Permissions.StorageRead>())
                //{
                //    // Mostrar un mensaje al usuario explicando por qué necesitas el permiso.
                //    await DisplayAlert("Permiso Necesario", "Se necesita acceso al almacenamiento para leer archivos. Por favor, concede el permiso.", "OK");                   
                //}

                var result = await Permissions.RequestAsync<Permissions.StorageRead>();

                if (result != PermissionStatus.Granted && count >=2)
                {
                    // Si el permiso fue denegado permanentemente, redirige al usuario a la configuración
                    bool goToSettings = await DisplayAlert("Permiso Denegado", "El permiso fue denegado permanentemente. Por favor, habilítalo en la configuración de la aplicación.", "Ir a Configuración", "Cancelar");
                    if (goToSettings)
                    {
                        AppInfo.Current.ShowSettingsUI();
                    }
                }

                status = result;
            }

            return status == PermissionStatus.Granted;
        }    

    }

}
