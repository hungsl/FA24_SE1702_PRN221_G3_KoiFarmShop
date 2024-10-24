using KoiFarmShop.Application.Implement.Service;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DTOs.Pet.AddPet;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace KoiFarmShop.WPFApp
{
    public partial class WindowPet : Window
    {
        private readonly IPetServiceLogic _petServiceLogic;

        public WindowPet(IPetServiceLogic petServiceLogic)
        {
            InitializeComponent();
            _petServiceLogic = petServiceLogic;
            LoadData();
        }

        public async void LoadData()
        {
            var petsResult = (await _petServiceLogic.GetAllPetAsync()).Object as List<Pet>;
            grdPets.ItemsSource = petsResult;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                {
                    MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var pet = new AddPetRequest
                {
                    Name = txtName.Text,
                    Age = int.Parse(txtAge.Text),
                    Gender = txtGender.Text,
                    ImageUrl = txtImageUrl.Text,
                    Color = txtColor.Text,
                    Length = double.Parse(txtLength.Text),
                    Weight = double.Parse(txtWeight.Text),
                    Quantity = int.Parse(txtQuantity.Text),
                    Note = txtNote.Text,
                    HealthStatus = int.Parse(txtHealthStatus.Text)
                };

                Guid? petId = string.IsNullOrEmpty(txtPetId.Text) ? (Guid?)null : Guid.Parse(txtPetId.Text);
                var result = petId.HasValue
                ? await _petServiceLogic.UpdatePetAsync(petId.Value, pet)
                : await _petServiceLogic.CreatePetAsync(pet);

                MessageBox.Show(result.IsSuccess ? "Save data success" : $"Failed to save data. Errors:\n{string.Join(Environment.NewLine, result.Errors.Select(e => e.Description))}",
                                result.IsSuccess ? "Success" : "Error",
                                MessageBoxButton.OK,
                                result.IsSuccess ? MessageBoxImage.Information : MessageBoxImage.Warning);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save pet information. Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInputs()
        {
            return !string.IsNullOrEmpty(txtName.Text) &&
                   !string.IsNullOrEmpty(txtAge.Text) &&
                   !string.IsNullOrEmpty(txtGender.Text) &&
                   !string.IsNullOrEmpty(txtColor.Text) &&
                   !string.IsNullOrEmpty(txtImageUrl.Text) &&
                   !string.IsNullOrEmpty(txtLength.Text) &&
                   !string.IsNullOrEmpty(txtWeight.Text) &&
                   !string.IsNullOrEmpty(txtQuantity.Text) &&
                   !string.IsNullOrEmpty(txtHealthStatus.Text);
        }
        private async void grdPets_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var pet = row.Item as Pet;
                    if (pet != null)
                    {
                        var petResult = await _petServiceLogic.GetPetByIdAsync(pet.Id);

                        if (petResult.IsSuccess && petResult.Object != null)
                        {
                            pet = petResult.Object as Pet;

                            txtPetId.Text = pet.Id.ToString();
                            txtName.Text = pet.Name;
                            txtAge.Text = pet.Age.ToString();
                            txtGender.Text = pet.Gender;
                            txtImageUrl.Text = pet.ImageUrl;
                            txtColor.Text = pet.Color;
                            txtLength.Text = pet.Length.ToString();
                            txtWeight.Text = pet.Weight.ToString();
                            txtQuantity.Text = pet.Quantity.ToString();
                            txtNote.Text = pet.Note;
                            txtHealthStatus.Text = pet.HealthStatus.ToString();

                        }
                    }
                }
            }
        }


        public void ReSet()
        {
            txtPetId.Text = string.Empty;
            txtAge.Text = string.Empty;
            txtName.Text = string.Empty;
            txtGender.Text = string.Empty;
            txtColor.Text = string.Empty;
            txtImageUrl.Text = string.Empty;
            txtLength.Text = string.Empty;
            txtWeight.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtNote.Text = string.Empty;
            txtHealthStatus.Text = string.Empty;
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            this.ReSet();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (grdPets.SelectedItem is Pet selectedPet)
            {
                var dialogResult = MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                if (dialogResult == MessageBoxResult.OK)
                {
                    var result = await _petServiceLogic.DeletePetAsync(selectedPet.Id);

                    MessageBox.Show(result.IsSuccess ? "Delete data success" : "Delete data fail");
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Please select a pet to delete");
            }
        }

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var petName = txtSearchPetName.Text.Trim();
                var color = txtSearchColor.Text.Trim();
                var healthStatus = txtSearchHealthStatus.Text.Trim();

                var petResult = (await _petServiceLogic.GetAllPetAsync()).Object as List<Pet>;

                var petFilter = petResult?.Where(pet =>
                (string.IsNullOrEmpty(petName) || pet.Name.Contains(petName, StringComparison.OrdinalIgnoreCase)) &&(string.IsNullOrEmpty(color) || pet.Color.Contains(color, StringComparison.OrdinalIgnoreCase)) &&(string.IsNullOrEmpty(healthStatus) || pet.HealthStatus.ToString().Contains(healthStatus, StringComparison.OrdinalIgnoreCase))
                ).ToList();

                grdPets.ItemsSource = petFilter;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while searching for pets: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
