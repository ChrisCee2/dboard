using CommunityToolkit.Mvvm.ComponentModel;

namespace dboard.ViewModels;

public partial class TextEditViewModel : ObservableObject
{
    [ObservableProperty]
    public string _text;

    public TextEditViewModel(string text)
    {
        Text = text;
    }

    public TextEditViewModel Clone()
    {
        return new TextEditViewModel(Text);
    }

    public void Copy(TextEditViewModel textEditToCopy)
    {
        Text = textEditToCopy.Text;
    }
}
