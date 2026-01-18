# FakeDataGen

FakeDataGen é um projeto voltado para **geração de dados falsos (fake data)** de forma **determinística, validada e configurável**, com foco em **testes, desenvolvimento, automação e ambientes não produtivos**.

O objetivo principal é fornecer uma API/CLI capaz de gerar dados realistas — como **CNPJ, CPF e outros identificadores brasileiros** — respeitando regras oficiais de validação, além de permitir variações como **formatação, pontuação e caracteres alfanuméricos**.

---

## ✨ Principais Funcionalidades

* Geração de dados fake válidos
* Suporte a regras oficiais (ex: cálculo de dígitos verificadores)
* Configuração via parâmetros (query/CLI)
* Separação clara de responsabilidades (Arquitetura Hexagonal)
* Core desacoplado de frameworks
* Ideal para testes automatizados, mocks e seeds de banco

---

## 🧱 Arquitetura

O projeto segue o padrão de **Arquitetura Hexagonal (Ports and Adapters)**:

### 🧠 Core

* Contém regras de negócio puras
* Não depende de frameworks
* Exemplo: `Cnpj` como **Value Object** responsável por validação e geração

---

## 🧾 Exemplo de Geração de CNPJ

Parâmetros comuns:

* `quantity`: quantidade de registros
* `punctuation`: com ou sem máscara (`00.000.000/0000-00`)
* `alphanumeric`: permite letras na base

Exemplo (API):

```
GET /fake-data/cnpj?quantity=10&punctuation=true&alphanumeric=false
```

---

## 🛠️ Tecnologias Utilizadas

* .NET (Minimal API / Console App)
* C#
* Arquitetura Hexagonal
* Injeção de Dependência
* Testes automatizados

---

## 🧪 Casos de Uso

* Testes unitários e de integração
* Seeds de banco de dados
* Mock de APIs externas
* Ambientes de QA e homologação
* Estudos de arquitetura e DDD

---
## 📌 Boas Práticas Adotadas

* Value Objects imutáveis
* Validação no domínio
* Sem lógica de negócio em controllers
* Código testável e extensível

---

---

## 👤 Autor

**Vando Rodrigues**
Desenvolvedor .NET

---
