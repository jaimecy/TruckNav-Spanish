const fs = require("fs");
const path = require("path");
const electronPath = require("electron");

const distDir = path.dirname(electronPath);
const source = path.join(distDir, "electron.exe");
const target = path.join(distDir, "TruckNavSpanish.exe");

function ensure() {
    if (!fs.existsSync(source)) {
        throw new Error(
            `electron.exe no encontrado en: ${distDir}. Ejecuta \`npm install\` en electron/ primero.`,
        );
    }

    let needsCopy = !fs.existsSync(target);

    if (!needsCopy) {
        const sourceStat = fs.statSync(source);
        const targetStat = fs.statSync(target);
        needsCopy =
            sourceStat.mtimeMs > targetStat.mtimeMs ||
            sourceStat.size !== targetStat.size;
    }

    if (needsCopy) {
        fs.copyFileSync(source, target);
        console.log(
            "[dev-exe] Copiado " +
                path.basename(source) +
                " -> " +
                path.basename(target),
        );
    }

    return target;
}

if (require.main === module) {
    console.log(ensure());
}

module.exports = ensure;