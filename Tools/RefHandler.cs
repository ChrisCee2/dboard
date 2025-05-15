using System.Text.Json.Serialization;

namespace dboard.Tools;

class RefHandler : ReferenceHandler
{
    public RefHandler() => Reset();
    private ReferenceResolver? _rootedResolver;
    public override ReferenceResolver CreateResolver() => _rootedResolver!;
    public void Reset() => _rootedResolver = new RefResolver();
}