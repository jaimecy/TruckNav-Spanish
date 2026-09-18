const cp = require("child_process");
const ensureDevExe = require("./ensure-dev-exe.cjs");

const exe = ensureDevExe();

const child = cp.spawn(exe, process.argv.slice(2), { stdio: "inherit" });

child.on("error", (err) => {
    console.error("[start-dev] Error al lanzar la aplicación:", err);
    process.exit(1);
});

child.on("exit", (code) => {
    process.exit(code ?? 0);
});