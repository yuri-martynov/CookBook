#r "System.Net.Http"
#r "Newtonsoft.Json"
#r "System.Runtime.Serialization"
#r "CookBook"
#r "Contract"
#r "Data"

//#if !COMPILED
//#I "../../bin/Binaries/WebJobs.Script.Host"
//#r "Microsoft.Azure.WebJobs.Host.dll"
//#endif

open System.Net
open System.Net.Http
open Newtonsoft.Json
open System.Runtime.Serialization
open System
//open Microsoft.Azure.WebJobs.Host

type Parameters = {
    dish: string;
    product: string
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

let private getDishById id = async { return DishDataAccess.getById id }
let private getDishesByIngredients products = async { return DishDataAccess.getByIngredients products }


let private answer (result: Result) : Async<string> =
    let dish = result.parameters.dish
    match result.action with
    | "recipe" -> Recipe.get getDishById dish
    | "ingredients" -> Ingredients.get getDishById dish
    | "contains" -> Contains.get getDishById dish result.parameters.product
    | "time" ->  Time.get getDishById dish // ????
    | "dish" -> Dish.findByIngredients getDishesByIngredients getDishById [result.parameters.product]

let Run(req: HttpRequestMessage, log: TraceWriter) =
    async {


        log.Info("Webhook was triggered!")
        let! jsonContent = req.Content.ReadAsStringAsync() |> Async.AwaitTask

        try
            let recipe = JsonConvert.DeserializeObject<RecipeRequest>(jsonContent)
            let! answer' =  answer recipe.result
            return req.CreateResponse(HttpStatusCode.OK, 
                { displayText = "From webhook"; speech = answer' })
        with e ->
            log.Info(e.ToString())
            return req.CreateResponse(HttpStatusCode.BadRequest)

    } |> Async.StartAsTask


