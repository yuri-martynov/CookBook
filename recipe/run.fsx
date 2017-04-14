﻿#r "System.Net.Http"
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

let Run(req: HttpRequestMessage, log: TraceWriter) =
    async {
        log.Info("Webhook was triggered!")
        let! jsonContent = req.Content.ReadAsStringAsync() |> Async.AwaitTask

        try
            let recipe = JsonConvert.DeserializeObject<RecipeRequest>(jsonContent)
            return req.CreateResponse(HttpStatusCode.OK, 
                { displayText = "From webhook"; speech = Recipe.get recipe.result.parameters.dish })
        with _ ->
            return req.CreateResponse(HttpStatusCode.BadRequest)
    } |> Async.StartAsTask
