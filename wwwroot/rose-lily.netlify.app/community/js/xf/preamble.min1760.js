var XF = window.XF || {};
! function(g, e) {
    var f = e.documentElement,
        k = f.getAttribute("data-cookie-prefix") || "",
        l = f.getAttribute("data-app"),
        m = "true" === f.getAttribute("data-logged-in");
    f.addEventListener("error", function(a) {
        a = a.target;
        switch (a.getAttribute("data-onerror")) {
            case "hide":
                a.style.display = "none";
                break;
            case "hide-parent":
                a.parentNode.style.display = "none"
        }
    }, !0);
    XF.Feature = function() {
        function a(b) {
            var a = f.className;
            d && (a = a.replace(/(^|\s)has-no-js($|\s)/, "$1has-js$2"), d = !1);
            b.length && (a += " " + b.join(" "));
            f.className =
                a
        }
        var b = {
                touchevents: function() {
                    return "ontouchstart" in g || g.DocumentTouch && e instanceof DocumentTouch
                },
                passiveeventlisteners: function() {
                    var a = !1;
                    try {
                        var b = Object.defineProperty({}, "passive", {
                                get: function() {
                                    a = !0
                                }
                            }),
                            c = function() {};
                        g.addEventListener("test", c, b);
                        g.removeEventListener("test", c, b)
                    } catch (n) {}
                    return a
                },
                hiddenscroll: function() {
                    var a = e.body,
                        b = !1;
                    a || (a = e.createElement("body"), e.body = a, b = !0);
                    var c = e.createElement("div");
                    c.style.width = "100px";
                    c.style.height = "100px";
                    c.style.overflow = "scroll";
                    c.style.position = "absolute";
                    c.style.top = "-9999px";
                    a.appendChild(c);
                    var d = c.offsetWidth === c.clientWidth;
                    b ? a.parentNode.removeChild(a) : c.parentNode.removeChild(c);
                    return d
                }
            },
            c = {},
            d = !0;
        return {
            runTests: function() {
                var h = [],
                    d;
                for (d in b)
                    if (b.hasOwnProperty(d) && "undefined" === typeof c[d]) {
                        var e = !!b[d]();
                        h.push("has-" + (e ? "" : "no-") + d);
                        c[d] = e
                    }
                a(h)
            },
            runTest: function(b, d) {
                d = !!d();
                a(["has-" + (d ? "" : "no-") + b]);
                c[b] = d
            },
            has: function(a) {
                return "undefined" === typeof c[a] ? (console.error("Asked for unknown test results: " +
                    a), !1) : c[a]
            }
        }
    }();
    XF.Feature.runTests();
    "public" !== l || m || function() {
        var a = (a = (a = (new RegExp("(^| )" + k + "notice_dismiss=([^;]+)(;|$)")).exec(e.cookie)) ? decodeURIComponent(a[2]) : null) ? a.split(",") : [];
        for (var b, c = [], d = 0; d < a.length; d++) b = parseInt(a[d], 10), 0 !== b && c.push('.notice[data-notice-id="' + b + '"]');
        c.length && (a = c.join(", ") + " { display: none !important } ", b = e.createElement("style"), b.type = "text/css", b.innerHTML = a, e.head.appendChild(b))
    }();
    (function() {
        var a = navigator.userAgent.toLowerCase(),
            b;
        if (b =
            /trident\/.*rv:([0-9.]+)/.exec(a)) b = {
            browser: "msie",
            version: parseFloat(b[1])
        };
        else {
            b = /(msie)[ \/]([0-9\.]+)/.exec(a) || /(edge)[ \/]([0-9\.]+)/.exec(a) || /(chrome)[ \/]([0-9\.]+)/.exec(a) || /(webkit)[ \/]([0-9\.]+)/.exec(a) || /(opera)(?:.*version|)[ \/]([0-9\.]+)/.exec(a) || 0 > a.indexOf("compatible") && /(mozilla)(?:.*? rv:([0-9\.]+)|)/.exec(a) || [];
            if ("webkit" == b[1] && a.indexOf("safari")) {
                var c = /version[ \/]([0-9\.]+)/.exec(a);
                b = c ? [b[0], "safari", c[1]] : (c = / os ([0-9]+)_([0-9]+)/.exec(a)) ? [b[0], "safari", c[1] +
                    "." + c[2]
                ] : [b[0], "safari", 0]
            }
            b = {
                browser: b[1] || "",
                version: parseFloat(b[2]) || 0
            }
        }
        b.browser && (b[b.browser] = !0);
        c = "";
        var d = null,
            e;
        if (/(ipad|iphone|ipod)/.test(a)) {
            if (c = "ios", e = /os ([0-9_]+)/.exec(a)) d = parseFloat(e[1].replace("_", "."))
        } else(e = /android[ \/]([0-9\.]+)/.exec(a)) ? (c = "android", d = parseFloat(e[1])) : /windows /.test(a) ? c = "windows" : /linux/.test(a) ? c = "linux" : /mac os/.test(a) && (c = "mac", 1 < navigator.maxTouchPoints && "MacIntel" === navigator.platform && (c = "ios"));
        b.os = c;
        b.osVersion = d;
        c && (b[c] = !0);
        f.className +=
            (b.os ? " has-os-" + b.os : "") + (b.browser ? " has-browser-" + b.browser : "");
        XF.browser = b
    })()
}(window, document);