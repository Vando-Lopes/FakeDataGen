using System.ComponentModel.DataAnnotations;

namespace FakeDataGen.Api.Contracts.Requests;

public sealed record GenerateCpfRequest([Range(1, 100)] int Quantity, bool Punctuation = true);
