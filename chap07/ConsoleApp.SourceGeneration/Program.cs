

ConfigureConsole(culture: "fr-FR");

decimal price = 19.99M;
DateTimeOffset today = DateTimeOffset.Now;

WriteLine($"Today, {today:D}, the price is {price:C}.");