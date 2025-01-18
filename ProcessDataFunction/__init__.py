import json
import logging

def main(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Processing HTTP request.')

    try:
        req_body = req.get_json()
        name = req_body.get('name')
        age = req_body.get('age')

        if name and age:
            response = {
                "message": f"Hello {name}, you are {age} years old!"
            }
            return func.HttpResponse(json.dumps(response), status_code=200)
        else:
            return func.HttpResponse(
                "Invalid input. Please provide 'name' and 'age'.",
                status_code=400
            )
    except ValueError:
        return func.HttpResponse(
            "Invalid JSON format.",
            status_code=400
        )