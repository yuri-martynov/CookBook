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

type Recipe = {
    result: Result
}

type Greeting = {
    Greeting: string
}

let Run(req: HttpRequestMessage, log: TraceWriter) =
    async {
        log.Info("Webhook was triggered!")
        let! jsonContent = req.Content.ReadAsStringAsync() |> Async.AwaitTask

        try
            let recipe = JsonConvert.DeserializeObject<Recipe>(jsonContent)
            return req.CreateResponse(HttpStatusCode.OK, 
                { Greeting = sprintf "Hello %s %s!" recipe.result.parameters.dish })
        with _ ->
            return req.CreateResponse(HttpStatusCode.BadRequest)
    } |> Async.StartAsTask
