### Azure Functions Project with Steps and Code

Below is an example project to create, test, and deploy an Azure Function using **HTTP Trigger** to process and return data. This project assumes you’re using **Visual Studio Code** with the **Azure Functions Extension**.

---

### **Project Goal**
Create an Azure Function that accepts a JSON payload via an HTTP POST request, processes the data, and returns a response.

---

### **Steps**

#### 1. **Set Up Your Environment**
- Install **Visual Studio Code**.
- Install the **Azure Functions Core Tools**.
- Install the **Azure CLI**.
- Install the **Azure Functions Extension** in VS Code.

#### 2. **Create an Azure Functions Project**
1. Open VS Code.
2. Use the Azure Functions extension:
   - Click on **"Azure"** in the Activity Bar.
   - Select **"Create New Project"**.
3. Choose a directory for your project.
4. Select a **language** (e.g., **Python**, **C#**, **JavaScript**, etc.).
5. Choose the **template**: `HTTP Trigger`.
6. Provide a function name: `ProcessDataFunction`.
7. Set authorization level to `Anonymous` for testing.

---

#### 3. **Code the Function**

##### **Default Code Directory Structure**
```
project/
│
├── ProcessDataFunction/
│   ├── function.json
│   ├── __init__.py  (for Python)
│   ├── index.js     (for JavaScript)
│   ├── main.cs      (for C#)
├── requirements.txt (Python dependencies)
├── local.settings.json
```

##### **Code Example: Python**
Edit `ProcessDataFunction/__init__.py`:

```python
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
```

---

#### 4. **Run Locally**
1. Open a terminal and navigate to your project folder.
2. Run the following command:
   ```bash
   func start
   ```
3. Test the function using a tool like **Postman** or **cURL**:
   ```bash
   curl -X POST http://localhost:7071/api/ProcessDataFunction \
        -H "Content-Type: application/json" \
        -d '{"name": "Atul", "age": 30}'
   ```

---

#### 5. **Deploy to Azure**
1. **Login to Azure**:
   ```bash
   az login
   ```
2. **Deploy the Function**:
   - In VS Code, click on the **Azure icon** in the Activity Bar.
   - Right-click your function app and select **"Deploy to Function App"**.
   - Create a new Function App when prompted.

---

#### 6. **Test in Azure**
- Get the function's **URL** from the Azure Portal or VS Code.
- Test using the same JSON payload as above.

---

#### 7. **Enhance with Bindings**
You can add input/output bindings (e.g., Cosmos DB, Blob Storage) by modifying the `function.json` file and connecting your function to Azure resources.

##### Example of `function.json`:
```json
{
  "bindings": [
    {
      "authLevel": "anonymous",
      "type": "httpTrigger",
      "direction": "in",
      "name": "req",
      "methods": ["post"]
    },
    {
      "type": "http",
      "direction": "out",
      "name": "$return"
    }
  ]
}
```

---

### **Code Repository**
Create a **GitHub repository** for version control. Push your project using the following:

```bash
git init
git add .
git commit -m "Initial commit for Azure Functions project"
git branch -M main
git remote add origin <your-repo-url>
git push -u origin main
```

---

This is a basic setup for Azure Functions. Let me know if you'd like to explore advanced topics like bindings, deployment pipelines, or integrating with other Azure services!
Here’s how you can run the Azure Functions project locally:

---

### **Prerequisites**

1. **Install Required Tools**:
   - [Visual Studio Code](https://code.visualstudio.com/)
   - [Azure Functions Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local#install-the-azure-functions-core-tools)
   - [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli)
   - Language-specific runtime:
     - Node.js (for JavaScript)
     - Python (for Python)
     - .NET SDK (for C#)

2. **Install Extensions**:
   - In Visual Studio Code, install the **Azure Functions** extension.

3. **Verify Installation**:
   Run these commands in your terminal to check installations:
   ```bash
   func --version   # Azure Functions Core Tools
   az --version     # Azure CLI
   ```

---

### **Steps to Run Locally**

#### **1. Open the Project in VS Code**
- Open your Azure Functions project folder in **Visual Studio Code**.

---

#### **2. Install Dependencies**
- Navigate to your project folder in the terminal.
- Install the dependencies:
  - **JavaScript**:  
    ```bash
    npm install
    ```
  - **Python**:  
    ```bash
    pip install -r requirements.txt
    ```

---

#### **3. Configure Local Settings**
- Ensure `local.settings.json` has the correct configuration:
  - **Node.js (JavaScript)**:
    ```json
    {
      "IsEncrypted": false,
      "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "node"
      }
    }
    ```
  - **Python**:
    ```json
    {
      "IsEncrypted": false,
      "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "python"
      }
    }
    ```
  - **C#**:
    ```json
    {
      "IsEncrypted": false,
      "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet"
      }
    }
    ```

---

#### **4. Start the Azure Function**
Run the following command in the terminal:
```bash
func start
```

This will start the Azure Functions host and display the local URL for your HTTP Trigger function, such as:
```
http://localhost:7071/api/ProcessDataFunction
```

---

#### **5. Test the Function**
Use a tool like **Postman**, **cURL**, or your browser to test the function.

- **Postman** (for HTTP POST):
  - Set the method to `POST`.
  - URL: `http://localhost:7071/api/ProcessDataFunction`
  - Body (JSON):
    ```json
    {
      "name": "Atul",
      "age": 30
    }
    ```

- **cURL**:
  ```bash
  curl -X POST http://localhost:7071/api/ProcessDataFunction \
       -H "Content-Type: application/json" \
       -d '{"name": "Atul", "age": 30}'
  ```

---

#### **6. Deploy to Azure (Optional)**
After verifying it works locally, you can deploy it to Azure using **Visual Studio Code** or the Azure CLI:
- **From VS Code**:
  - Click the **Azure icon** in the Activity Bar.
  - Right-click your Function App and select **Deploy to Function App**.

- **Using Azure CLI**:
  ```bash
  func azure functionapp publish <Your-Function-App-Name>
  ```

Replace `<Your-Function-App-Name>` with the name of your Azure Function App.

---

Let me know if you encounter any issues during setup or testing!
