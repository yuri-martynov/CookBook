#r "System.Net.Http"
#r "Newtonsoft.Json"
#r "System.Runtime.Serialization"
#r "CookBook"

open System.Net
open System.Net.Http
open Newtonsoft.Json
open System.Runtime.Serialization

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

[<DataContract>]
type RecipeResponse = {
    [<field: DataMember(Name="speech")>]
    speech: string;
    [<field: DataMember(Name="displayText")>]
    displayText: string;
}


let answer (result: Result) : Async<string> =
    let dish = result.parameters.dish
    match result.action with
    | "recipe" -> Recipe.get dish
    | "ingradients" -> Ingradients.get dish

let Run(req: HttpRequestMessage, log: TraceWriter) =
    async {
        log.Info("Webhook was triggered!")
        let! jsonContent = req.Content.ReadAsStringAsync() |> Async.AwaitTask

        try
            let recipe = JsonConvert.DeserializeObject<RecipeRequest>(jsonContent)
            let! answer' =  answer recipe.result
            return req.CreateResponse(HttpStatusCode.OK, 
                { displayText = "From webhook"; speech = answer' })
        with _ ->
            return req.CreateResponse(HttpStatusCode.BadRequest)

    } |> Async.StartAsTask


