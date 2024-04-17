"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Try = exports.Err = exports.Ok = void 0;
function Ok(result) {
    if (typeof result !== "object" || result === null) {
        throw new Error("Ok() must be passed an object");
    }
    Object.defineProperty(result, "ok", {
        value: true,
        enumerable: true,
        writable: false,
    });
    return result;
}
exports.Ok = Ok;
function Err(error) {
    var result = { error: error };
    Object.defineProperty(result, "ok", {
        value: false,
        enumerable: true,
        writable: false,
    });
    return result;
}
exports.Err = Err;
function Try(fnOrPromise) {
    if (isPromise(fnOrPromise)) {
        return fnOrPromise.then(Ok).catch(Err);
    }
    try {
        var result = fnOrPromise();
        if (isPromise(result)) {
            return result.then(Ok).catch(Err);
        }
        return Ok(result !== null && result !== void 0 ? result : {});
    }
    catch (error) {
        return Err(error);
    }
}
exports.Try = Try;
/**
 * Whether the given value is a promise.
 *
 * This is required because some libraries (e.g. Prisma) have their own Promise
 * implementation that doesn't inherit from the global Promise.
 *
 * This is rare, but you'll have to handle any libraries where this is the case
 * in this function.
 */
function isPromise(x) {
    return (x instanceof Promise ||
        (typeof x === "object" &&
            "then" in x &&
            (x[Symbol.toStringTag] === "PrismaPromise" || x[Symbol.toStringTag] === "Promise")));
}
