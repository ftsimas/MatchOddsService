using MatchOddsService.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchOddsService.Factories
{
    public static class ErrorDTOFactory
    {
        public static ErrorDTO ModelBindingError(KeyValuePair<string, ModelStateEntry> keyValuePair)
        {
            var key = keyValuePair.Key;
            string detail;

            if (key == "$")
            {
                detail = "Request body is not in valid JSON format.";
            }
            else if (key.Length >= 2 && key.StartsWith("$."))
            {
                var formattedKey = key.Length >= 3 ? Char.ToUpper(key[2]) + key[3..] : key;
                detail = $"Data type of field {formattedKey} is not valid.";
            }
            else
            {
                detail = keyValuePair.Value.Errors.First().ErrorMessage;
            }

            return new ErrorDTO
            {
                Code = "E0001",
                Message = "Binding Error",
                Detail = detail
            };
        }

        public static ErrorDTO DataValidationError(string key) => new ErrorDTO
        {
            Code = "E0002",
            Message = "Validation Error",
            Detail = $"Value of field {key} is not valid."
        };

        public static ErrorDTO NotFoundError(KeyValuePair<string, object> keyValuePair) => new ErrorDTO
        {
            Code = "E0003",
            Message = "Resource Not Found",
            Detail = $"Requested resource {keyValuePair.Key} with ID '{keyValuePair.Value}' does not exist."
        };

        public static ErrorDTO IdMismatchError() => new ErrorDTO
        {
            Code = "E0004",
            Message = "ID Mismatch",
            Detail = $"IDs referenced in URI and in request body do not match."
        };

        public static ErrorDTO ForeignKeyError(KeyValuePair<string, object>? keyValuePair = null)
        {
            var detail = keyValuePair == null
                ? "Request body contains a ID reference that does not exist."
                : $"Referenced {keyValuePair.Value.Key} with value '{keyValuePair.Value.Value}' does not exist.";

            return new ErrorDTO
            {
                Code = "E0005",
                Message = "Reference Not Found",
                Detail = detail
            };
        }

        public static ErrorDTO ForeignKeyRestrictionOnDeleteError(KeyValuePair<string, object> keyValuePair, string referencingResourceName)
        {
            var formattedReferencingResourceName = referencingResourceName != null
                ? $"{referencingResourceName} "
                : "";
            return new ErrorDTO
            {
                Code = "E0006",
                Message = "Resource Deletion Restriction",
                Detail = $"Resource {keyValuePair.Key} with ID '{keyValuePair.Value}' is being referenced by other {formattedReferencingResourceName}resources and cannot be deleted."
        };
        }

        public static ErrorDTO UniqueIndexError() => new ErrorDTO
        {
            Code = "E0007",
            Message = "Unique Value Combination Error",
            Detail = "A combination of values that is required to be unique already exists."
        };

        public static object ErrorCollectionDTO(ICollection<ErrorDTO> errors) => errors.Count switch
        {
            0 => null,
            1 => errors.First(),
            _ => new { errors }
        };
    }
}
