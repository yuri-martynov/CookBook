#r "System.Net.Http"
#r "Newtonsoft.Json"

open System.Net
open System.Net.Http
open Newtonsoft.Json

type Parameters = {
    dish: string
}

type Result = {
    action: string;
    parameters: Parameters
}

type RecipeRequest = {
    result: Result
}

[<CLIMutable>]
type RecipeResponse = {
    speech: string;
    displayText: string;
}

let Run(req: HttpRequestMessage, log: TraceWriter) =
    async {
        log.Info("Webhook was triggered!")
        let! jsonContent = req.Content.ReadAsStringAsync() |> Async.AwaitTask

        let recipe = JsonConvert.DeserializeObject<RecipeRequest>(jsonContent)
        let bodyObj = { displayText = "From webhook"; speech = sprintf "Мы вас научим готовить %s!" recipe.result.parameters.dish }
        let body = JsonConvert.SerializeObject(bodyObj)
        return body
    } |> Async.StartAsTask
