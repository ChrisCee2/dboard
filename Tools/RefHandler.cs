using System.Text.Json.Serialization;

namespace mystery_app.Tools;

class RefHandler : ReferenceHandler
{
    public RefHandler() => Reset();
    private ReferenceResolver? _rootedResolver;
    public override ReferenceResolver CreateResolver() => _rootedResolver!;
    public void Reset() => _rootedResolver = new RefResolver();
}