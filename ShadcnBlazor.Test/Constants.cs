namespace ShadcnBlazor.Test;

public class Constants
{
    public const string NpmInitContent = "mkdir Styles && cd Styles\nnpm init -y";

    public const string NpmInstallPostcssContent =
        "npm install tailwindcss @tailwindcss/postcss postcss postcss-cli tw-animate-css";

    public const string PostCssContent = "export default {\n  plugins: {\n    \"@tailwindcss/postcss\": {},\n  }\n}";
    public const string InstallNugetContent = "dotnet add package ShadcnBlazor\ndotnet add package ShadcnBlazor.Extras";
    public const string PortalRazor = "<!-- MainLayout.razor -->\n<PortalOutlet />";

    public const string ServiceCollectionContent =
        "// Program.cs\nusing ShadcnBlazor;\nusing ShadcnBlazor.Extras;\n\nbuilder.Services.AddShadcnBlazor();\nbuilder.Services.AddShadcnBlazorExtras();";

    public const string StylesContent =
        "@import \"tailwindcss\";\n@import \"tw-animate-css\";\n\n@import \"../bin/ShadcnBlazor/scrollbar.css\";\n@import \"../bin/ShadcnBlazor/default-theme.css\";\n@source \"../bin/ShadcnBlazor/ShadcnBlazor.map\";\n\n@source \"../**/*.razor\";\n@source \"../**/*.cs\";\n@source \"../**/*.html\";\n\n@custom-variant dark (&:is(.dark *));\n\n#blazor-error-ui {\n    display: none;\n}\n\n@layer base {\n    * {\n        @apply border-border outline-ring/50;\n    }\n    body {\n        @apply bg-background text-foreground;\n    }\n}";

    public const string HtmlContent =
        "...\n<link rel=\"stylesheet\" href=\"/style.min.css\"/>\n...\n\n\n...\n<script src=\"/_content/ShadcnBlazor/interop.js\" defer />\n<script src=\"/_content/ShadcnBlazor.Extras/interop.js\" defer />\n...";

    public const string BuildCommandContent = "npx postcss styles.css -o ../wwwroot/style.min.css --watch";
}