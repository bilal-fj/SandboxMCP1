using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text;
using System.Text.Json;

namespace McpServer.Tools;

[McpServerToolType]
public sealed class SandboxTool
{
    [McpServerTool, Description("Gets the amount of sand in a sand box")]
    public static string getSandAmount()
    {
        return "10kg";
    }

    [McpServerTool, Description("Generates a Report using a Word Template")]
    public async Task<string> generateReport(string address,
        string backgroundMentalHealthWellbeing,
        string physicalHealth,
        string mentalAndEmotionalWellbeing,
        string whatIsImportantToYou,
        string managingAndMaintainingNutrition,
        string maintainingPersonalHygiene,
        string managingToiletNeeds,
        string beingAppropriatelyClothed,
        string maintainingAHabitableHomeEnvironment)
    {
        string flowUrl = "https://156d13aa02a2e8e3a7ac8808fdc323.a6.environment.api.powerplatform.com:443/powerautomate/automations/direct/workflows/9587f4ea00c8487c899aa1aa285ba201/triggers/manual/paths/invoke/?api-version=1&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=UuMdVfqGIzifWQA1ARUVw7pw9DETysrn1OopSsJx0TI";

        // Create the request body based on the provided JSON schema
        var requestBody = new
        {
            Address = address,
            BackgroundMentalHealthWellbeing = backgroundMentalHealthWellbeing,
            PhysicalHealth = physicalHealth,
            MentalAndEmotionalWellbeing = mentalAndEmotionalWellbeing,
            WhatIsImportantToYou = whatIsImportantToYou,
            ManagingAndMaintainingNutrition = managingAndMaintainingNutrition,
            MaintainingPersonalHygiene = maintainingPersonalHygiene,
            ManagingToiletNeeds = managingToiletNeeds,
            BeingAppropriatelyClothed = beingAppropriatelyClothed,
            MaintaingAHabitableHomeEnvironment = maintainingAHabitableHomeEnvironment
        };

        // Serialize the request body to JSON
        string jsonContent = JsonSerializer.Serialize(requestBody);

        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Create the HTTP content with JSON
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Send the POST request
                HttpResponseMessage response = await client.PostAsync(flowUrl, content);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Flow triggered successfully!");
                    Console.WriteLine($"Response: {responseBody}");
                    return "Flow triggered successfully (check Power Automate for details).";
                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error triggering flow: {response.StatusCode}");
                    Console.WriteLine($"Error details: {errorResponse}");
                    return $"Error: Could not trigger flow. Status: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return $"An exception occurred: {ex.Message}";
            }
        }
    }
}
 
