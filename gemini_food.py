import argparse
import os
import sys
from typing import Optional

import google.generativeai as genai
from dotenv import load_dotenv


DEFAULT_MODEL = "gemini-1.5-flash-latest"


def configure_genai_from_env() -> None:
	load_dotenv()
	api_key = os.getenv("GEMINI_API_KEY")
	if not api_key:
		raise RuntimeError(
			"GEMINI_API_KEY not set. Add it to your environment or .env file."
		)
	genai.configure(api_key=api_key)


def build_client(model: str):
	return genai.GenerativeModel(model)


def ask_food_question(question: str, model: str = DEFAULT_MODEL, system_instruction: Optional[str] = None) -> str:
	configure_genai_from_env()
	client = build_client(model)

	prompt_parts = []
	if system_instruction:
		prompt_parts.append(system_instruction)
	prompt_parts.append(question)

	response = client.generate_content(prompt_parts)
	text = response.text or ""
	return text.strip()


def parse_args(argv: Optional[list[str]] = None) -> argparse.Namespace:
	parser = argparse.ArgumentParser(
		description="Ask Gemini a food-related question and print the answer."
	)
	parser.add_argument(
		"question",
		nargs=argparse.REMAINDER,
		help="Your question for Gemini (e.g., best high-protein vegetarian dinners).",
	)
	parser.add_argument(
		"--model",
		default=DEFAULT_MODEL,
		help=f"Model name to use (default: {DEFAULT_MODEL}).",
	)
	parser.add_argument(
		"--system",
		dest="system_instruction",
		help="Optional system instruction to guide tone/style (e.g., reply concisely).",
	)
	return parser.parse_args(argv)


def main(argv: Optional[list[str]] = None) -> int:
	args = parse_args(argv)
	if not args.question:
		print("Please provide a question. Example: python gemini_food.py What are quick high-protein lunches?", file=sys.stderr)
		return 2
	question = " ".join(args.question).strip()
	try:
		answer = ask_food_question(question, model=args.model, system_instruction=args.system_instruction)
		print(answer)
		return 0
	except Exception as exc:
		print(f"Error: {exc}", file=sys.stderr)
		return 1


if __name__ == "__main__":
	sys.exit(main())