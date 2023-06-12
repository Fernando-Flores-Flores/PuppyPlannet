﻿using System.Text.Json.Serialization;

namespace BackPuppy.Validaciones
{
    public class DetalleProblema
    {
        //
        // Resumen:
        //     A URI reference [RFC3986] that identifies the problem type. This specification
        //     encourages that, when dereferenced, it provide human-readable documentation for
        //     the problem type (e.g., using HTML [W3C.REC-html5-20141028]). When this member
        //     is not present, its value is assumed to be "about:blank".
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        //
        // Resumen:
        //     A short, human-readable summary of the problem type.It SHOULD NOT change from
        //     occurrence to occurrence of the problem, except for purposes of localization(e.g.,
        //     using proactive content negotiation; see[RFC7231], Section 3.4).
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        //
        // Resumen:
        //     The HTTP status code([RFC7231], Section 6) generated by the origin server for
        //     this occurrence of the problem.
        [JsonPropertyName("statusCode")]
        public int? Status { get; set; }
        //
        // Resumen:
        //     A human-readable explanation specific to this occurrence of the problem.
        [JsonPropertyName("details")]
        public List<ErrorValidacion>? Detail { get; set; }
        //
        // Resumen:
        //     A URI reference that identifies the specific occurrence of the problem.It may
        //     or may not yield further information if dereferenced.
        [JsonPropertyName("instance")]
        public string? Instance { get; set; }
        //
        // Resumen:
        //     Gets the System.Collections.Generic.IDictionary`2 for extension members.
        //     Problem type definitions MAY extend the problem details object with additional
        //     members. Extension members appear in the same namespace as other members of a
        //     problem type.
        //
        // Comentarios:
        //     The round-tripping behavior for Microsoft.AspNetCore.Mvc.ProblemDetails.Extensions
        //     is determined by the implementation of the Input \ Output formatters. In particular,
        //     complex types or collection types may not round-trip to the original type when
        //     using the built-in JSON or XML formatters.
        [JsonExtensionData]
        public IDictionary<string, object>? Extensions { get; }
    }
}
