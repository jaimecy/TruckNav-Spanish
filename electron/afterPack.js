const { execFile } = require("child_process");
const { promisify } = require("util");
const fs = require("fs");
const path = require("path");

const execFileAsync = promisify(execFile);

function findRcedit() {
    const localAppData = process.env.LOCALAPPDATA || "";
    const candidates = [
        path.join(__dirname, "node_modules", "electron-winstaller", "vendor", "rcedit.exe"),
        path.join(localAppData, "electron-builder", "Cache", "winCodeSign", "854208499", "rcedit-x64.exe"),
    ];

    const cacheRoot = path.join(localAppData, "electron-builder", "Cache", "winCodeSign");
    if (fs.existsSync(cacheRoot)) {
        for (const entry of fs.readdirSync(cacheRoot)) {
            candidates.push(path.join(cacheRoot, entry, "rcedit-x64.exe"));
        }
    }

    return candidates.find((candidate) => fs.existsSync(candidate));
}

exports.default = async function afterPack(context) {
    if (context.electronPlatformName !== "win32") {
        return;
    }

    const exeName = `${context.packager.appInfo.productFilename}.exe`;
    const exePath = path.join(context.appOutDir, exeName);
    const iconPath = path.join(
        context.packager.projectDir,
        "assets",
        "TruckNavIconOutline.ico",
    );
    const rcedit = findRcedit();

    if (!rcedit) {
        throw new Error("No se encontró rcedit.exe para incrustar el icono de TruckNav.");
    }

    if (!fs.existsSync(exePath) || !fs.existsSync(iconPath)) {
        throw new Error(`Falta el EXE o el icono: ${exePath} / ${iconPath}`);
    }

    await execFileAsync(rcedit, [exePath, "--set-icon", iconPath]);
};
