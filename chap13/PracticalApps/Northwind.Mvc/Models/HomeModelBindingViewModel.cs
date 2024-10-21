using Northwind.Mvc.Models;

namespace Northwind.Mvc;

public record HomeModelBindingViewModel(Thing Thing, bool HasErrors, IEnumerable<string> ValidationErrors);

