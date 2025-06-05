using CommunityToolkit.Mvvm.ComponentModel;

namespace dboard.ViewModels;

public partial class TextEditViewModel : ObservableObject
{
    [ObservableProperty]
    public string _text;
    [ObservableProperty]
    private bool _isWordWrap;

    public TextEditViewModel(string text, bool isWordWrap = true)
    {
        Text = text;
        IsWordWrap = isWordWrap;
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
