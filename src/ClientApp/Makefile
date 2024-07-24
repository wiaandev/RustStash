all: lint build

.PHONY:	dependencies
dependencies:
ifdef CI
	npm ci
else
	npm install
endif

.PHONY:	lint
lint:
ifdef	CI
	npm run lint
else
	npm run lint --fix
endif

.PHONY:	typecheck
typecheck:
	npm run typecheck

.PHONY:	build
build:
	npm run build