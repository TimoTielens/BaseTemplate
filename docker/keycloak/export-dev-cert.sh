#!/usr/bin/env bash
# Exports the trusted ASP.NET Core HTTPS dev certificate as PEM for the
# Keycloak container to serve on https://localhost:8082 when running the
# backing services via docker-compose. Run once before `docker compose up`.
set -euo pipefail

# Resolve the repo root regardless of where this script is invoked from.
script_dir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cert_dir="${script_dir}/certs"

mkdir -p "${cert_dir}"

# Ensure the dev cert exists and is trusted by the OS (and browser).
dotnet dev-certs https --trust

# PEM export produces keycloak.crt + keycloak.key (key unencrypted via --no-password).
dotnet dev-certs https --format PEM --no-password -ep "${cert_dir}/keycloak.crt"

echo "Exported dev certificate to ${cert_dir}/keycloak.crt and keycloak.key"
