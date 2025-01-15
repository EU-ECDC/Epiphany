# Epiphany

Epiphany is a tool designed to promote understanding and experimentation with generative artificial intelligence (AI) models in a secure environment. It was initially developed for ECDC staff and is now shared as an open-source project.

## Features

- **Dual Assistants**:
  - **GenAI**: General capabilities, similar to OpenAI's ChatGPT.
  - **EpiAI**: Tailored for problem-solving in the field of epidemiology.
- **Prompt Submission**: Users can submit a prompt and receive a response from the AI Service.
- **Compliance**: Fully adheres to the European Union Data Protection Regulation (EUDPR).

## Technology Stack

- **Backend**: .NET Core with Razor Pages
- **Security**: Designed to be deployed in a secure environment, requiring integration with the organization's own Azure-hosted OpenAI instance.

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/EU-ECDC/Epiphany.git
   cd Epiphany
   ```
2. Set up your Azure environment:
   - Ensure you have access to OpenAI hosted within your Azure tenant.
   - Configure your API keys and endpoints in the application settings file.
3. Build and run the application:
   ```bash
   cd src
   dotnet build
   dotnet run --project ChatTest
   ```

## Usage

- Access the tool via your browser at the specified localhost or hosted URL.
- Use GenAI for general queries and EpiAI for epidemiological problem-solving.
- Do not share sensitive or personal data while using the tool.

## License

Details are available in the [LICENSE.txt](./LICENSE.txt) file.

## Authors and Acknowledgments

- Developed by the Digital Transformation Services - Digital Solutions at ECDC.
- For more information, visit [ECDC's website](https://www.ecdc.europa.eu).

## Disclaimer

Epiphany is intended for educational and experimental use. Outputs should not be used in public or legally binding documents and must always be critically assessed for accuracy, biases, and compliance with intellectual property rights.
