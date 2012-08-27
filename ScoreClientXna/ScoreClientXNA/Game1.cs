using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using HtmlAgilityPack;
using System.Net;
using System.Text;
using System.IO;

namespace ScoreClientXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        SpriteFont fontBold;
        SpriteFont fontItalic;
        String webPage;
        String rendertext;
        String[] renderArray;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Font");
            fontBold = Content.Load<SpriteFont>("FontBold");
            fontItalic = Content.Load<SpriteFont>("FontItalic");

            renderArray = File.ReadAllLines(@"render.txt");

            StreamReader reader = new StreamReader(@"render.txt");
            rendertext = reader.ReadToEnd();
            reader.Close();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
           // webPage = Get();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //spriteBatch.DrawString(font, webPage, new Vector2(10, 10), Color.Black);
            //ParseHTML(spriteBatch, rendertext); //this is the HTMLParse(String)
            ParseHTML(spriteBatch, renderArray);//HTMLparse(string[])
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public String Get()
        {
            string uriString = @"http://localhost:49707/Score";
            using (WebClient webCLient = new WebClient())
            {
                webCLient.Headers["User-Agent"] =
                    "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
                    "(compatible; MSIE 6.0; Windows NT 5.1; " +
                    ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";

                //download the data
                byte[] pageArray = webCLient.DownloadData(uriString);

                //And return the response after parsing it
                return Encoding.ASCII.GetString(pageArray);
            }

        }

        public void ParseHTML(SpriteBatch spriteBatch, String pageData)
        {
            /* use this to callf rom the main DRAW function in Game1
             *  this will draw this string based on the tag that prefixes it.
             *  if <b> use bold font, if <i> use italic font
             *  still need to figure out how to prse and draw the table struture.
             */
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageData);

            IEnumerable<HtmlNode> nodes = doc.DocumentNode.ChildNodes;

            int num = 0; // keeps track of how many strings it has drawn and draws i
            foreach (HtmlNode node in nodes)
            {
                if (node.Name == "b")
                {
                    spriteBatch.DrawString(fontBold, node.InnerText, new Vector2(0, num * 14), Color.Black);
                    num++;
                }

                if (node.Name == "i")
                {
                    spriteBatch.DrawString(fontItalic, node.InnerText, new Vector2(0, num * 14), Color.Black);
                    num++;
                }
                if (node.Name == "p")
                {
                    spriteBatch.DrawString(font, node.InnerText, new Vector2(12, num * 14), Color.Black); //012 instead of 0 to rep the indent
                    num++;
                    num++; //do this for a line break between paragraphs?
                }
            }
            
        }

        public void ParseHTML(SpriteBatch spriteBatch, String[] pageData)
        {
            int num = 0; // keeps track of how many strings it has drawn and draws i
            HtmlDocument doc = new HtmlDocument();
            for (int i = 0; i < pageData.Length; i++)
            {
                doc.LoadHtml(pageData[i]);

                IEnumerable<HtmlNode> nodes = doc.DocumentNode.ChildNodes;

                
                foreach (HtmlNode node in nodes)
                {
                    if (node.Name == "b")
                    {
                        spriteBatch.DrawString(fontBold, node.InnerText, new Vector2(0, num * 14), Color.Black);
                        num++;
                    }

                    if (node.Name == "i")
                    {
                        spriteBatch.DrawString(fontItalic, node.InnerText, new Vector2(0, num * 14), Color.Black);
                        num++;
                    }
                    if (node.Name == "p")
                    {
                        spriteBatch.DrawString(font, node.InnerText, new Vector2(12, num * 14), Color.Black); //012 instead of 0 to rep the indent
                        num++;
                        num++; //do this for a line break between paragraphs?
                    }
                }
            }//end for loop
        }
    }
}
