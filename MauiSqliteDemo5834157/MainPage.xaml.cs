namespace MauiSqliteDemo5834157
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        private int _editClienteId;
        int count = 0;

        public MainPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            Task.Run(async () => lisView.ItemsSource = await _dbService.GetCliente());
        }


        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            if (_editClienteId == 0)
            {
                await _dbService.Create(new Cliente
                {
                    NombreCliente = nombreEntryField.Text,
                    Email = emailEntryField.Text,
                    Movil = movilEntryField.Text
                });

            }
            else
            {
                await _dbService.Update(new Cliente
                {
                    Id = _editClienteId,
                    NombreCliente = nombreEntryField.Text,
                    Email = emailEntryField.Text,
                    Movil = movilEntryField.Text
                }) ;
                _editClienteId = 0;
            }
            nombreEntryField.Text = string.Empty;
            emailEntryField.Text = string.Empty;
            movilEntryField.Text = string.Empty;

            lisView.ItemsSource = await _dbService.GetCliente();

        }
    

        private async void lisView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var clientes = (Cliente)e.Item;
            var action = await DisplayActionSheet("action", "cancel", null, "edit", "delete");

            switch (action)
            {
                case "edit":
                    _editClienteId = clientes.Id;
                    nombreEntryField.Text = clientes.NombreCliente;
                    emailEntryField.Text = clientes.Email;
                    movilEntryField.Text = clientes.Movil;
                    break;

                case "delete":
                    await _dbService.Delete(clientes);
                    lisView.ItemsSource = await _dbService.GetCliente();
                    break;
            }

        }
    }

}
