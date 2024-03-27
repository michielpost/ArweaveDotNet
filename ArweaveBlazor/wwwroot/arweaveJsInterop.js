// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.
import {
    message,
    result,
    results,
    dryrun,
    createDataItemSigner as webSigner
} from "https://www.unpkg.com/@permaweb/aoconnect@0.0.49/dist/browser.js";

import { } from "https://www.unpkg.com/arbundles@0.11.0/build/web/bundle.js";

import { } from 'https://unpkg.com/arweave/bundles/web.bundle.min.js';

let arweave;

export function loadJs(sourceUrl) {
    if (sourceUrl.Length == 0) {
        console.error("Invalid source URL");
        return;
    }

    var tag = document.createElement('script');
    tag.src = sourceUrl;
    tag.type = "text/javascript";

    //tag.onload = function () {
    //    console.log("Script loaded successfully");
    //}

    tag.onerror = function () {
        console.error("Failed to load script");
    }

    document.body.appendChild(tag);
}

export async function InitArweave() {
    arweave = Arweave.init({});
}

export async function HasArConnect() {
    if (window.arweaveWallet) {
        return true;
    }
    else {
        return false;
    }
};

export async function GenerateWallet() {
    var result = await arweave.wallets.generate();
    console.log(result);
    return JSON.stringify(result);
}

export async function GetAddress(jwk) {
    var jwkJson = JSON.parse(jwk);
    var result = await arweave.wallets.getAddress(jwkJson);
    console.log(result);
    return result;
}

export async function GetWalletBalance(address) {
    var result = await arweave.wallets.getBalance(address)
    return result;
}

export async function ConnectArConnect(permissions, appInfo) {
    var result = await window.arweaveWallet.connect(permissions, appInfo)
    return result;
}

export async function Disconnect() {
    await window.arweaveWallet.disconnect()
}

export async function GetActiveAddress(permissions, appInfo) {

    try {
        var result = await window.arweaveWallet.getActiveAddress()
        return result;
    }
    catch { }
    return null;
}

export function createDataItemSigner(wallet) {
    const signer = async ({ data, tags, target, anchor }) => {
        const signer = new Arbundles.ArweaveSigner(wallet)
        const dataItem = Arbundles.createData(data, signer, { tags, target, anchor })
        return dataItem.sign(signer)
            .then(async () => ({
                id: await dataItem.id,
                raw: await dataItem.getRaw()
            }))
    }

    return signer
}

export async function Send(jwk, processId, data, tags) {
    var signer;

    if (jwk != null) {
        wallet = JSON.parse(jwk);
        signer = createDataItemSigner(wallet);
    }
    else {
        var wallet = window.arweaveWallet;
        signer = webSigner(wallet);
    }


    try {
        let result = await message({
            process: processId,
            tags: tags,
            signer: signer,
            data: data,
        });

        return result;
    } catch (error) {
        console.error(error);
    }

}


export async function SendDryRun(processId, data, tags) {
    //let tags = [
    //    { name: "Your-Tag-Name-Here", value: "your-tag-value" },
    //    { name: "Another-Tag", value: "another-value" },
    //];
    //let signer = createDataItemSigner(window.arweaveWallet);

    try {
        let { Messages, Spawns, Output, Error } = await dryrun({
            process: processId,
            tags: tags,
            //signer: signer,
            data: data,
        });

        //console.log(Messages);
        if (Messages.length > 0) {
            return Messages[0].Data;
        }
        return null;

    } catch (error) {
        console.error(error);
    }

}


export async function GetResult(processId, msgId) {
    let { Messages, Spawns, Output, Error } = await result({
        // the arweave TXID of the message
        message: msgId,
        // the arweave TXID of the process
        process: processId,
    });

    //console.log(Messages);

    if (Messages.length > 0) {
        return Messages[0].Data;
    }
    return null;
}

export async function GetResults(processId, limit) {
    // fetching the first page of results
    let resultsOut = await results({
        process: processId,
        sort: "ASC",
        limit: limit,
    });

   console.log(resultsOut);
}

export async function SaveFile(fileName, fileContent) {
    const blob = new Blob([fileContent], { type: 'application/json' });
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = fileName;
    link.click();
    window.URL.revokeObjectURL(link.href);
}

