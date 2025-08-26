## Gemini Food CLI

Ask the Gemini API questions about food from the command line.

### Setup

1. Create a `.env` file using the template:

```
cp .env.example .env
```

2. Edit `.env` and set your key:

```
GEMINI_API_KEY=your_api_key_here
```

3. Install dependencies (ideally in a virtualenv):

```
pip install -r requirements.txt
```

### Usage

Examples:

```
python gemini_food.py What are quick high-protein vegetarian dinners?
```

Specify a model:

```
python gemini_food.py --model gemini-1.5-pro-latest Plan a 3-day low-carb menu
```

Provide a system instruction for tone or formatting:

```
python gemini_food.py --system "Reply concisely with bullet points" Suggest kid-friendly veggie snacks
```

### Bash alternative (no Python deps)

Set your API key and run the script:

```
chmod +x gemini_food.sh
export GEMINI_API_KEY=your_api_key_here
./gemini_food.sh Suggest a 2-day vegetarian menu
```

Choose a model:

```
MODEL=gemini-1.5-pro ./gemini_food.sh What are high-protein vegan dinners?
```