﻿using System.Text;

namespace NightlyCode.Japi.Json {

    /// <summary>
    /// extension methods for <see cref="JsonNode"/>s
    /// </summary>
    public static class JsonExtensions {

        /// <summary>
        /// converts the json node to a more human readable format
        /// </summary>
        /// <param name="json"><see cref="JsonNode"/> to format</param>
        /// <returns>a formatted string</returns>
        public static string Format(this JsonNode json) {
            StringBuilder sb = new StringBuilder();
            AnalyseJson(json, 0, sb, false);
            return sb.ToString();
        }

        static void AnalyseJson(JsonNode json, int indent, StringBuilder sb, bool indentfirst)
        {
            bool first = true;
            if (json is JsonObject)
            {
                JsonObject @object = (JsonObject)json;

                if(indentfirst)
                    sb.Append(new string(' ', indent));

                sb.Append("{");
                sb.AppendLine();
                foreach (string key in @object.Keys)
                {
                    if(!first) {
                        sb.Append(",");
                        sb.AppendLine();
                    }
                    else first = false;

                    sb.Append(new string(' ', indent + 2));
                    sb.Append(key);
                    sb.Append(" : ");

                    JsonNode node = @object[key];
                    AnalyseJson(node, indent + 2, sb, false);
                }
                sb.AppendLine();
                sb.Append(new string(' ', indent));
                sb.Append("}");
            }
            else if (json is JsonArray)
            {
                JsonArray array = (JsonArray)json;

                if (indentfirst)
                    sb.Append(new string(' ', indent));

                sb.Append("[");
                foreach (JsonNode value in array.Items)
                {
                    if(!first)
                        sb.Append(",");
                    else {
                        first = false;
                    }

                    AnalyseJson(value, indent + 2, sb, false);
                }
                sb.Append("]");
            }
            else if (json is JsonValue)
            {
                JsonValue value = (JsonValue)json;
                if (value.Value == null)
                    sb.Append("null");
                else if (value.Value is string)
                    sb.Append("\"" + (string)value.Value + "\"");
                else sb.Append(value.Value.ToString());
            }
        }

    }
}