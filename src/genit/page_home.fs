module page_home

open canopy
open canopyExtensions

let baseuri = "http://localhost:8083/"
let uri name = baseuri + name

//selectors

let private _count name = sprintf "//span[@class='sidebar-item' and text()='%s']/../span[2]" name
let _applications_count = _count "Applications" |> xpath
let _suites_count = _count "Suites" |> xpath
let _testCases_count = _count "Test Cases" |> xpath
let _testRuns_count = _count "Test Runs" |> xpath

let _hiName name = sprintf "Hi %s!" name |> text
let _login = css "a[href='/login']"
