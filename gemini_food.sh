#!/usr/bin/env bash
set -euo pipefail

MODEL=${MODEL:-"gemini-1.5-flash"}
API_KEY=${GEMINI_API_KEY:-${API_KEY:-}}

if [[ -z "${API_KEY}" ]]; then
	echo "GEMINI_API_KEY is not set. Export it or set API_KEY." >&2
	exit 2
fi

if [[ $# -eq 0 ]]; then
	echo "Usage: GEMINI_API_KEY=... MODEL=gemini-1.5-pro ./gemini_food.sh \"Suggest a 2-day vegetarian menu\"" >&2
	exit 2
fi

QUESTION="$*"

# Using the responses:generateContent endpoint
URL="https://generativelanguage.googleapis.com/v1beta/models/${MODEL}:generateContent?key=${API_KEY}"

read -r -d '' BODY <<'JSON'
{
  "contents": [
    {
      "role": "user",
      "parts": [
        { "text": "__QUESTION__" }
      ]
    }
  ]
}
JSON

# Escape quotes in question for JSON safety
BODY=${BODY/__QUESTION__/${QUESTION//\"/\\\"}}
BODY=${BODY/__QUESTION__/${QUESTION//"/\"}}

curl -sS -X POST \
	-H "Content-Type: application/json" \
	"${URL}" \
	-d "${BODY}" | python3 - <<'PY'
import sys, json
try:
	data = json.load(sys.stdin)
	cands = data.get("candidates") or []
	if cands:
		parts = cands[0].get("content", {}).get("parts", [])
		if parts and isinstance(parts[0], dict) and "text" in parts[0]:
			print(parts[0]["text"]) 
			sys.exit(0)
	print(json.dumps(data, ensure_ascii=False, indent=2))
except Exception as e:
	print(f"Failed to parse response: {e}", file=sys.stderr)
	sys.exit(1)
PY