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

//[<DataContract>]
type RecipeResponse = {
    //[<field: DataMember(Name="speech")>]
    speech: string;
    //[<field: DataMember(Name="displayText")>]
    displayText: string;
}

let Run(req: HttpRequestMessage, log: TraceWriter) =
    async {
        log.Info("Webhook was triggered!")
        let! jsonContent = req.Content.ReadAsStringAsync() |> Async.AwaitTask

        GlobalConfiguration.Configuration.Formatters <- DefaultContractResolver()

        try
            let recipe = JsonConvert.DeserializeObject<RecipeRequest>(jsonContent)
            return req.CreateResponse(HttpStatusCode.OK, 
                { displayText = "From webhook"; speech = sprintf "Мы вас научим готовить %s!" recipe.result.parameters.dish })
        with _ ->
            return req.CreateResponse(HttpStatusCode.BadRequest)
    } |> Async.StartAsTask
