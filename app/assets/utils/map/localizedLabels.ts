import {
    SPANISH_COUNTRY_TOKENS,
    SPANISH_MAP_LABELS,
} from "~/data/spanishMapLabels";

export function localizeMapName(name: string, locale: string): string {
    if (locale !== "es" || !name) return name;
    return SPANISH_MAP_LABELS[name] ?? name;
}

export function localizeCountryToken(token: string, locale: string): string {
    if (!token) return "";

    const normalized = token.toLowerCase();
    if (locale === "es" && SPANISH_COUNTRY_TOKENS[normalized]) {
        return SPANISH_COUNTRY_TOKENS[normalized];
    }

    return token.replace(/_/g, " ").replace(/\b\w/g, (c) => c.toUpperCase());
}

export function localizedMapTextField(locale: string) {
    if (locale !== "es") {
        return ["get", "name"];
    }

    const expression: unknown[] = ["case"];
    for (const [englishName, spanishName] of Object.entries(SPANISH_MAP_LABELS)) {
        expression.push(["==", ["get", "name"], englishName], spanishName);
    }
    expression.push(["get", "name"]);
    return expression;
}
