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

type RecipeResponse = {
    speech: string;
}

let Run(req: HttpRequestMessage, log: TraceWriter) =
    async {
        log.Info("Webhook was triggered!")
        let! jsonContent = req.Content.ReadAsStringAsync() |> Async.AwaitTask

        try
            let recipe = JsonConvert.DeserializeObject<RecipeRequest>(jsonContent)
            return req.CreateResponse(HttpStatusCode.OK, 
                { speech = sprintf "Мы вас научим готовить %s!" recipe.result.parameters.dish })
        with _ ->
            return req.CreateResponse(HttpStatusCode.BadRequest)
    } |> Async.StartAsTask
